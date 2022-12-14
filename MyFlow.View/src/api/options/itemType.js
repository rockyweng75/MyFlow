import request from '@/utils/request'

export const fetchItemTypes = () => {
    return request({
        url: '/api/ItemType',
        method: 'get',
    })
}