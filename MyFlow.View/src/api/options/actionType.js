import request from '@/utils/request'

export const fetchActionTypes= () => {
    return request({
        url: '/api/ActionType',
        method: 'get',
    })
}