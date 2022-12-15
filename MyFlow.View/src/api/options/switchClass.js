import request from '@/utils/request'

export const fetchSwitchClasses = () => {
    return request({
        url: '/api/Switch/',
        method: 'get',
    })
}