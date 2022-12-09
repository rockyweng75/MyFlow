import request from '@/utils/request'

export const fetchDeadline = () => {
    return request({
        url: '/api/Deadline',
        method: 'get',
    })
}