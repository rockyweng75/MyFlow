
/**
 * @param {string} path
 * @returns {Boolean}
 */
const isExternal = (path) => {
  return /^(https?:|mailto:|tel:)/.test(path)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validUsername = (str) => {
  const reg = /^[A-Z0-9]+$/
  return reg.test(str)
}

/**
 * @param {string} url
 * @returns {Boolean}
 */
const validURL = (url) => {
  const reg = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/
  return reg.test(url)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validLowerCase = (str) => {
  const reg = /^[a-z]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validUpperCase = (str) => {
  const reg = /^[A-Z]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validAlphabets = (str) => {
  const reg = /^[A-Za-z]+$/
  return reg.test(str)
}

/**
 * @param {string} email
 * @returns {Boolean}
 */
const validEmail = (email) => {
  const reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
  return reg.test(email)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validNumber = (str) => {
  const reg = /^[0-9]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validAuthCode = (str) => {
  const reg = /^[0-9]+$/
  return reg.test(str) && str.length == 10
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const validPhone = (rule, str, callback) => {
  const reg = /^[0-9]+$/
  if(reg.test(str) && str.length == 10)
  {
    callback()
  } else {
    callback(new Error(rule.message))
  }
}

/** 
* @param {string} str
* @returns {Boolean}
*/
const validJson = (str) => {
  try {
    JSON.parse(str);
  } catch (e){
      return false;
  }
  return true;
}


/**
 * @param {string} str
 * @returns {Boolean}
 */
const validCurrentSubj = (str) => {
  const reg = /^[A-Z0-9]+$/
  return reg.test(str) && str.length == 4
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
const isString = (str) => {
  if (typeof str === 'string' || str instanceof String) {
    return true
  }
  return false
}

/**
 * @param {Array} arg
 * @returns {Boolean}
 */
const isArray = (arg) => {
  if (typeof Array.isArray === 'undefined') {
    return Object.prototype.toString.call(arg) === '[object Array]'
  }
  return Array.isArray(arg)
}

export default {
  isExternal,
  validUsername,
  validAlphabets,
  validAuthCode,
  validCurrentSubj,
  validEmail,
  validLowerCase,
  validNumber,
  validPhone,
  validURL,
  validUpperCase,
  validUsername,
  isArray,
  isString,
  validJson,
}
