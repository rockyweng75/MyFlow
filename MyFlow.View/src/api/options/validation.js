import request from '@/utils/request'

export const fetchValidation = () => {
    return request({
        url: '/api/Validation',
        method: 'get',
    })
}