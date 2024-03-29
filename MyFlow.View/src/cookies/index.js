import Cookies from 'js-cookie'

const TokenKey = 'Admin-Token'

export function getToken() {
  return Cookies.get(TokenKey);
}

export function getTokenAsync() {
  return new Promise((resolve, reject) => {
    let token = Cookies.get(TokenKey)
    console.log('cookies', token)
    resolve(token)
  })
}

export function setToken(token) {
  return Cookies.set(TokenKey, token)
}

export function removeToken() {
  return Cookies.remove(TokenKey)
}
