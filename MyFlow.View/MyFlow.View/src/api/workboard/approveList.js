import request from '@/utils/request'

export const fetchApproveList = (pager) => {
    return request({
        url: '/api/ApproveHistory',
        method: 'get',
        params: pager
    })
}

export const fetchDistincts = (columnName) =>{
    return request({
        url: '/api/ApproveHistory/Distinct',
        method: 'get',
        params: { CurrentFilter: columnName}
    })
}