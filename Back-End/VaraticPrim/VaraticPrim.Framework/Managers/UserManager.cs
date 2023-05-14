using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Email;
using VaraticPrim.Email.Gmail;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Extentions;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Repository.Repository.Interfaces;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Framework.Managers;

public class UserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateModel> _userCreateValidator;
    private readonly IValidator<UserUpdateModel> _userUpdateValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserManager> _logger;
    private readonly IHashService _hashService;
    private readonly IMailingService _mailingService;

    public UserManager(
        IUserRepository userRepository, 
        IMapper mapper, 
        IValidator<UserCreateModel> userCreateValidator, 
        IValidator<UserUpdateModel> userUpdateValidator,
        ILogger<UserManager> logger, 
        IHashService hashService,
        IMailingService mailingService)
    {
        _hashService = hashService;
        _mailingService = mailingService;
        _userCreateValidator = userCreateValidator;
        _userUpdateValidator = userUpdateValidator;
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> Create(UserCreateModel userCreateModel)
    {
        try
        {
            _logger.LogInformation("Creating user...");
            await _userCreateValidator.ValidateAndThrowAsync(userCreateModel);
            
            if (await _userRepository.EmailExists(userCreateModel.Email))
            {
                _logger.LogWarning($"User with email = {userCreateModel.Email} already exists");
                throw new UserAlreadyExistsException($"User with email = {userCreateModel.Email} already exists");
            }

            var userEntity = _mapper.Map<UserEntity>(userCreateModel);
            var passwordSalt = _hashService.GenerateSalt();

            userEntity.PasswordHash = _hashService.Hash(userCreateModel.Password, passwordSalt);
            userEntity.PasswordSalt = passwordSalt;

            await _userRepository.Insert(userEntity);

            var userModel = _mapper.Map<UserModel>(userEntity);
            _logger.LogInformation("User created.");


            var htmlBody = @"<!DOCTYPE html>
            <html>
            <head>

              <meta charset='utf-8'>
              <meta http-equiv='x-ua-compatible' content='ie=edge'>
              <title>Email Confirmation</title>
              <meta name='viewport' content='width=device-width, initial-scale=1'>
              <style type='text/css'>

              @media screen {
                @font-face {
                  font-family: 'Source Sans Pro';
                  font-style: normal;
                  font-weight: 400;
                  src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
                }
                @font-face {
                  font-family: 'Source Sans Pro';
                  font-style: normal;
                  font-weight: 700;
                  src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
                }
              }

              body,
              table,
              td,
              a {
                -ms-text-size-adjust: 100%; /* 1 */
                -webkit-text-size-adjust: 100%; /* 2 */
              }

              table,
              td {
                mso-table-rspace: 0pt;
                mso-table-lspace: 0pt;
              }

              img {
                -ms-interpolation-mode: bicubic;
              }

              a[x-apple-data-detectors] {
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                color: inherit !important;
                text-decoration: none !important;
              }

              div[style*='margin: 16px 0;'] {
                margin: 0 !important;
              }
              body {
                width: 100% !important;
                height: 100% !important;
                padding: 0 !important;
                margin: 0 !important;
              }

              table {
                border-collapse: collapse !important;
              }
              a {
                color: #1a82e2;
              }
              img {
                height: auto;
                line-height: 100%;
                text-decoration: none;
                border: 0;
                outline: none;
              }
              </style>

            </head>
            <body style='background-color: #e9ecef;'>

              <!-- start preheader -->
              <div class='preheader' style='display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;'>
                A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.
              </div>
              <!-- end preheader -->

              <!-- start body -->
              <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                <!-- start logo -->
                <tr>
                  <td align='center' bgcolor='#e9ecef'>
                    <!--[if (gte mso 9)|(IE)]>
                    <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
                    <tr>
                    <td align='center' valign='top' width='600'>
                    <![endif]-->
                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
                      <tr>
                        <td align='center' valign='top' style='padding: 36px 24px;'>
                          <a href='https://www.blogdesire.com' target='_blank' style='display: inline-block;'>
                            <img src='https://www.blogdesire.com/wp-content/uploads/2019/07/blogdesire-1.png' alt='Logo' border='0' width='48' style='display: block; width: 48px; max-width: 48px; min-width: 48px;'>
                          </a>
                        </td>
                      </tr>
                    </table>
                    <!--[if (gte mso 9)|(IE)]>
                    </td>
                    </tr>
                    </table>
                    <![endif]-->
                  </td>
                </tr>
                <!-- end logo -->

                <!-- start hero -->
                <tr>
                  <td align='center' bgcolor='#e9ecef'>
                    <!--[if (gte mso 9)|(IE)]>
                    <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
                    <tr>
                    <td align='center' valign='top' width='600'>
                    <![endif]-->
                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
                      <tr>
                        <td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;'>
                          <h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;'>Confirm Your Email Address</h1>
                        </td>
                      </tr>
                    </table>
                    <!--[if (gte mso 9)|(IE)]>
                    </td>
                    </tr>
                    </table>
                    <![endif]-->
                  </td>
                </tr>
                <!-- end hero -->

                <!-- start copy block -->
                <tr>
                  <td align='center' bgcolor='#e9ecef'>
                    <!--[if (gte mso 9)|(IE)]>
                    <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
                    <tr>
                    <td align='center' valign='top' width='600'>
                    <![endif]-->
                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

                      <!-- start copy -->
                      <tr>
                        <td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'>
                          <p style='margin: 0;'>Tap the button below to confirm your email address. If you didn't create an account with <a href='https://blogdesire.com'>Paste</a>, you can safely delete this email.</p>
                        </td>
                      </tr>
                      <!-- end copy -->

                      <!-- start button -->
                      <tr>
                        <td align='left' bgcolor='#ffffff'>
                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                            <tr>
                              <td align='center' bgcolor='#ffffff' style='padding: 12px;'>
                                <table border='0' cellpadding='0' cellspacing='0'>
                                  <tr>
                                    <td align='center' bgcolor='#1a82e2' style='border-radius: 6px;'>
                                      <a href='https://www.blogdesire.com' target='_blank' style='display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Do Something Sweet</a>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <!-- end button -->

                      <!-- start copy -->
                      <tr>
                        <td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'>
                          <p style='margin: 0;'>If that doesn't work, copy and paste the following link in your browser:</p>
                          <p style='margin: 0;'><a href='https://blogdesire.com' target='_blank'>https://blogdesire.com/xxx-xxx-xxxx</a></p>
                        </td>
                      </tr>
                      <!-- end copy -->

                      <!-- start copy -->
                      <tr>
                        <td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf'>
                          <p style='margin: 0;'>Cheers,<br> Paste</p>
                        </td>
                      </tr>
                      <!-- end copy -->

                    </table>
                    <!--[if (gte mso 9)|(IE)]>
                    </td>
                    </tr>
                    </table>
                    <![endif]-->
                  </td>
                </tr>
                <!-- end copy block -->

                <!-- start footer -->
                <tr>
                  <td align='center' bgcolor='#e9ecef' style='padding: 24px;'>
                    <!--[if (gte mso 9)|(IE)]>
                    <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
                    <tr>
                    <td align='center' valign='top' width='600'>
                    <![endif]-->
                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

                      <!-- start permission -->
                      <tr>
                        <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'>
                          <p style='margin: 0;'>You received this email because we received a request for [type_of_action] for your account. If you didn't request [type_of_action] you can safely delete this email.</p>
                        </td>
                      </tr>
                      <!-- end permission -->

                      <!-- start unsubscribe -->
                      <tr>
                        <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'>
                          <p style='margin: 0;'>To stop receiving these emails, you can <a href='https://www.blogdesire.com' target='_blank'>unsubscribe</a> at any time.</p>
                          <p style='margin: 0;'>Paste 1234 S. Broadway St. City, State 12345</p>
                        </td>
                      </tr>
                      <!-- end unsubscribe -->

                    </table>
                    <!--[if (gte mso 9)|(IE)]>
                    </td>
                    </tr>
                    </table>
                    <![endif]-->
                  </td>
                </tr>
                <!-- end footer -->

              </table>
              <!-- end body -->

            </body>
            </html>";
            
            
            _logger.LogInformation("Email result = " + _mailingService.SendEmail("plesca.virgiliu@gmail.com", "Email confirmation", htmlBody, $"{userModel.Contact.FirstName} {userModel.Contact.LastName}"));
              
            return userModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create user");
            throw;
        }
    }

    public async Task<UserModel> GetById(int id)
    {
        try
        {
            var userEntity = await _userRepository.GetById(id);
            if (userEntity == null)
            {
                _logger.LogWarning($"User with id = {id} not found");
                throw new UserNotFoundException($"User with id = {id} not found");
            }

            return _mapper.Map<UserModel>(userEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get user");
            throw;
        }
    }
    
    public async Task<UserModel> DeleteById(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            
            if (user == null)
            {
                _logger.LogWarning($"User with id = {id} not found");
                throw new UserNotFoundException($"User with id = {id} not found");
            }

            var userModel = _mapper.Map<UserModel>(user);
            
            await _userRepository.Delete(user);

            return userModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete user");
            throw;
        }
    }
    
    public async Task<UserModel> Update(UserUpdateModel userUpdateModel, int id)
    {
        try
        {
            await _userUpdateValidator.ValidateAndThrowAsync(userUpdateModel);
            var userFromDb = await _userRepository.GetById(id);

            if (userFromDb == null)
            {
                _logger.LogWarning($"User with id = {id} not found");
                throw new UserNotFoundException($"User with id = {id} not found");
            }

            userFromDb.Contact = _mapper.Map<ContactEntity>(userUpdateModel.Contact);
            
            await _userRepository.Update(userFromDb);

            return _mapper.Map<UserModel>(userFromDb);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update user");
            throw;
        }
    }

    public async Task<PagedListModel<UserModel>> GetAll(UserFilterModel filterModel)
    {
        //UserFiltermodel in UserFilter
        var pagedList = await _userRepository.GetAll(new UserFilter
        {
            PageIndex = filterModel.PageIndex,
            PageSize = filterModel.PageSize
        });
        
        return pagedList.Map(entity => _mapper.Map<UserModel>(entity));
    }
}