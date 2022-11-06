import request from '/@/utils/request'

export const fetchList = (data) => {
    return request({
        url: '/api/Flowchart',
        method: 'get',
        params: data
    })
}

export const fetchListByAdmin = (data) => {
    return request({
        url: '/api/Flowchart/Admin',
        method: 'get',
        params: data
    })
}

export const fetchOne = (data) => {
    return request({
        url: '/api/Flowchart/' + data,
        method: 'post',
    })
}


