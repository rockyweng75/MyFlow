import request from '@/utils/request'

export const fetchMemberList = () => {
    return request({
        url: '/Members',
        method: 'get',
    })
}



