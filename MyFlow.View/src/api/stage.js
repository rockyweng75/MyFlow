import request from '@/utils/request'

export const fetchStages = (flowId) => {
    return request({
        url: '/api/Stage/Flow/' + flowId,
        method: 'get',
    })
}

export const fetchStage = (model) => {
    return request({
        url: '/api/Stage/' + model.id + '/' + model.action,
        method: 'get',
    })
}



