import request from '@/utils/request'

export const fetchHistoryList = (pager) => {
    return request({
        url: '/api/ApplyHistory',
        method: 'get',
        params: pager
    })
}

export const fetchDistincts = (columnName) =>{
    return request({
        url: '/api/ApplyHistory/Distinct',
        method: 'get',
        params: { CurrentFilter: columnName}
    })
}