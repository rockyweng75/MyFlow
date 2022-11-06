import store from '/@/store'
import CryptoJS from 'crypto-js'

/**
 * @param {Array} value
 * @returns {Boolean}
 * @example see @/views/permission/directive.vue
 */
export function checkPermission(value) {
  if (value && value instanceof Array && value.length > 0) {
    const roles = store.getters && store.getters.roles
    const permissionRoles = value

    const hasPermission = roles.some(role => {
      return permissionRoles.includes(role)
    })
    return hasPermission
  } else {
    console.error(`need roles! Like v-permission="['admin','editor']"`)
    return false
  }
}

const key = CryptoJS.enc.Utf8.parse("1234123412ABCDEF"); //十六位十六進位數作為密鑰 
const iv = CryptoJS.enc.Utf8.parse('ABCDEF1234123412'); //十六位十六進位數作為密鑰偏移量

// export default function Decrypt(word) { 
//     let encryptedHexStr = CryptoJS.enc.Hex.parse(word); 
//     let srcs = CryptoJS.enc.Base64.stringify(encryptedHexStr);
//     let decrypt = CryptoJS.AES.decrypt(srcs, key, { iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 }); 
//     let decryptedStr = decrypt.toString(CryptoJS.enc.Utf8); 
//     return decryptedStr.toString(); 
// } 
//加密方法 
export function Encrypt(word) {
    let srcs = CryptoJS.enc.Utf8.parse(word); 
    let encrypted = CryptoJS.AES.encrypt(srcs, key, { iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 }); 
    return encrypted.ciphertext.toString().toUpperCase(); 
}
