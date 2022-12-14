import request from '@/utils/request'

export const fetchActionClasses = () => {
    return request({
        url: '/api/ActionClass/',
        method: 'get',
    })
}