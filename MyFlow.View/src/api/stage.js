import request from '@/utils/request'

export const fetchStages = (flowId) => {
    return request({
        url: '/api/Stage/Flow/' + flowId,
        method: 'get',
    })
}

export const fetchStage = (stageId) => {
    return request({
        url: '/api/Stage/' + model.id ,
        method: 'get',
    })
}



