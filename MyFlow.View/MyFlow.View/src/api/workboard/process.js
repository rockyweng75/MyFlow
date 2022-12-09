import request from '@/utils/request'

export const fetchAgree = (data) => {
    return request({
        url: '/Agree',
        method: 'post',
        data
    })
}

export const fetchDisagree = (data) => {
    return request({
        url: '/Disagree',
        method: 'post',
        data
    })
}

export const fetchSubmit = (data) => {
    return request({
        url: '/Submit',
        method: 'post',
        data
    })
}

export const fetchCancel = (data) => {
    return request({
        url: '/Cancel',
        method: 'post',
        data
    })
}

export const fetchActionList = () => {
    return request({
        url: '/api/Action',
        method: 'get',
    })
}
