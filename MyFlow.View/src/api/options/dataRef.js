import request from '@/utils/request'

export const fetchDataRefs = () => {
    return request({
        url: '/api/DataRef',
        method: 'get',
    })
}