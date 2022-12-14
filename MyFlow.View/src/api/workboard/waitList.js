import request from '@/utils/request'

export const fetchWaitList = (pager) => {
    return request({
        url: '/api/Wait',
        method: 'get',
        params: pager
    })
}

export const fetchDistincts = (columnName) =>{
    return request({
        url: '/api/Wait/Distinct',
        method: 'get',
        params: { CurrentFilter: columnName}
    })
}