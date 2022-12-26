import request from '@/utils/request'

export const fetchWeather = (location) => {
    return request({
        url: '/Weather',
        method: 'get',
        params: location
    })
}



