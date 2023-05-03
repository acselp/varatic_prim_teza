import axios from "./axios";
import {useAuthHeader, createRefresh} from 'react-auth-kit'
import {diffMinutes} from "../TimeService/Time";

const refreshApi = createRefresh({
    interval: 12,   // Refreshs the token in every 10 minutes
    refreshApiCallback: async ({   // arguments
            authToken,
            authTokenExpireAt,
            refreshToken,
            refreshTokenExpiresAt,
            authUserState
        }) => {
        try {

            const response = await axios.post("/authentication/refresh-token", {'refreshToken': refreshToken}, {
                headers: {'Authorization': `Bearer ${authToken}`}}
            )

            return {
                isSuccess: true,
                newAuthToken: response.data.accessToken,
                newAuthTokenExpireIn: diffMinutes(new Date(response.data.expiresIn), Date.now()),
                newRefreshTokenExpiresIn: diffMinutes(new Date(response.data.refreshTokenExpirationTime), Date.now())
            }
        }
        catch(error){
            console.error(error)
            return {
                isSuccess: false
            }
        }
    }
})

export default refreshApi