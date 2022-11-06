import axios from 'axios'
import { ElMessageBox, ElMessage } from 'element-plus'
import store from '/@/store'
import { getToken } from '/@/cookies/index.js'
import router from '/@/router'

// create an axios instance
const service = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  withCredentials: true, // send cookies when cross-domain requests
  timeout: 1000 * 60 * 3, // request timeout
  crossDomain: true,
  headers:{
    //'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Credentials': true,
    'Access-Control-Allow-Headers': 'X-Requested-With, Content-Type, Set-Cookie, cross-site',
    'Access-Control-Allow-Methods': 'PUT,POST,GET,DELETE,OPTIONS',
  }
})

// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent
    var token = store.getters['user/token'];
    if (token) {
      // let each request carry token
      // ['X-Token'] is a custom headers keygetters
      // please modify it according to the actual situation
      config.headers['authorization'] = "Bearer " + token
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response
    // if the custom code is not 20000, it is judged as an error.
    if (res.status !== 20000 && res.status !== 200) {
      Message({
        message: res.message || 'Error',
        type: 'error',
        duration: 5 * 1000
      })

      // 50008: Illegal token; 50012: Other clients logged in; 50014: Token expired;
      if (res.status === 50008 || res.status === 50012 || res.status === 50014 || res.status === 403) {
        // to re-login
        ElMessageBox.confirm('You have been logged out, you can cancel to stay on this page, or log in again', 'Confirm logout', {
          confirmButtonText: 'Re-Login',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }).then(() => {
          console.log('resetToken!!')
          store.dispatch('user/resetToken').then(() => {
            location.reload()
          })
        })
      }

      return Promise.reject(new Error(res.message || 'Error'))
    } else {
      return res.data
    }
  },
  error => {

    if(error.response.status == 401 )
    {
        ElMessage({
            message: '應用程式已閒置超時',
            type: 'error',
            duration: 5 * 1000
        })
        store.dispatch('security/resetToken').then(()=>
        {
          location.reload()
        })
    }


    return Promise.reject(error)
  }
)

// service.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
// service.defaults.headers.common['Access-Control-Allow-Credentials'] = 'true';
// service.defaults.headers.common['Access-Control-Allow-Headers'] = 'X-Requested-With, Content-Type';
// service.defaults.headers.common['Access-Control-Allow-Methods'] = 'PUT,POST,GET,DELETE,OPTIONS';

export default service
