import axios from 'axios'

export default async ({ Vue }) => {
  var http = axios.create({
    baseURL: 'https://localhost:5001/api/',
    timeout: 5000
  })
  Vue.prototype.$axios = http
}
