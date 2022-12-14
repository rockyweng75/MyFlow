import request from '@/utils/request'

export const fetchTodoList = (pager) => {
    return request({
        url: '/api/Todo',
        method: 'get',
        params: pager
    })
}

export const fetchDistincts = (columnName) =>{
    return request({
        url: '/api/Todo/Distinct',
        method: 'get',
        params: { CurrentFilter: columnName}
    })
}