import axios from "axios";

const MyAxios = axios.create({
    baseURL: 'http://localhost:5000'
});

export default MyAxios;