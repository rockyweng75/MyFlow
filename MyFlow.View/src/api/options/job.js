import request from '@/utils/request'

export const fetchJobs = () => {
    return request({
        url: '/api/Job',
        method: 'get'
    })
}