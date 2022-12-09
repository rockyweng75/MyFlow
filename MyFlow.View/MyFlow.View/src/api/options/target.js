import request from '@/utils/request'

export const fetchTargets = () => {
    return request({
        url: '/api/Target',
        method: 'get'
    })
}