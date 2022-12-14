import request from '@/utils/request'

export const fetchFlowTypes = () => {
    return request({
        url: '/api/FlowType',
        method: 'get',
    })
}