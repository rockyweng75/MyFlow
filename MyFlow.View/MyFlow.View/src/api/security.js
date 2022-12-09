import request from '@/utils/request'

export const login = (data) => {
    return request({
        url: '/signin',
        method: 'post',
        data
    })
}

export const logout = (data) => {
    return request({
        url: '/signout',
        method: 'post',
        data
    })
}

export function getUser(token) {
  var data = {data: token} 
  return request({
    url: '/user',
    method: 'post',
    data
  })
}

