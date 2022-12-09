import request from '@/utils/request'

export const fetchFlowcharts = () => {
    return request({
        url: '/api/Flowchart',
        method: 'get',
    })
}
export const fetchFlowchartsByAdmin = () => {
    return request({
        url: '/api/Flowchart/Admin',
        method: 'get',
    })
}

export const fetchFormByFlowId = (flowId) => {
    return request({
        url: '/api/Flowchart/Form/' + flowId,
        method: 'get',
    })
}

export const fetchFlowchart = (flowId) => {
    return request({
        url: '/api/Flowchart/' + flowId,
        method: 'get',
    })
}

export const create = (formdata) => {
    var data  = JSON.stringify(formdata)
    return request({
        url: '/api/Flowchart',
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
        url: '/api/Flowchart',
        method: 'put',
        headers: {
            'Content-Type': 'application/json',
        },
        data
    })
}