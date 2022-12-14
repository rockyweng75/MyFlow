import request from '@/utils/request'

export const fetchTitles = () => {
    return request({
        url: '/api/Title',
        method: 'get'
    })
}
