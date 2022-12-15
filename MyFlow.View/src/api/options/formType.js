import request from '@/utils/request'

export const fetchFormTypes = () => {
    return request({
        url: '/api/FormType',
        method: 'get',
    })
}