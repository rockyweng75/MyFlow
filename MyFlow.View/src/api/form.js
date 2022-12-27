import request from '@/utils/request'

export const fetchForms = () => {
    return request({
        url: '/api/Form',
        method: 'get',
    })
}

export const fetchApplyForm = (flowId) => {
    return request({
        url: '/api/Apply/' + flowId,
        method: 'get',
    })
}

export const fetchForm = (formId) => {
    return request({
        url: '/api/Form/' + formId,
        method: 'get',
    })
}

export const fetchStage = (stageId) => {
    return request({
        url: '/api/Stage/' + stageId,
        method: 'get',
    })
}


export function fetchFormV2(id) {
    var data = {data: id} 
    return request({
        url: '/api/Form',
        method: 'get',
        params: data
    })
}


export const create = (formdata) => {
    var data = JSON.stringify(formdata)
    return request({
        url: '/api/Form',
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
        },
        data
    })
}

export const update = (formdata) => {
    var data  = JSON.stringify(formdata)
    return request({
        url: '/api/Form',
        method: 'put',
        headers: {
            'Content-Type': 'application/json',
        },
        data
    })
}