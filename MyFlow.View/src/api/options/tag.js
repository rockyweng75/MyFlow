import request from '@/utils/request'

export const fetchTags = () => {
    return request({
        url: '/api/Tag',
        method: 'get'
    })
}