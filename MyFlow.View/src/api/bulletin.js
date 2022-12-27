import request from '@/utils/request'

export const fetchBulletinList = (query) => {
    return request({
        url: '/Bulletins',
        method: 'get',
        params: query
    })
}



