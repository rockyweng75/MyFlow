import request from '@/utils/request'

export const fetchUserRoles = () => {
    return request({
        url: '/api/Permission',
        method: 'get',
    })
}

export const fetchUser = (id) => {
    return request({
        url: '/api/Permission/' + id,
        method: 'get',
    })
}

export const create = (data) => {
    return request({
        url: '/api/Permission',
        method: 'post',
        data
    })
}

export const update = (data) => {
    return request({
        url: '/api/Permission',
        method: 'put',
        data
    })
}

export const remove = (data) => {
    return request({
        url: '/api/Permission',
        method: 'delete',
        data
    })
}