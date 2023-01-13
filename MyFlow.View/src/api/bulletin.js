import request from '@/utils/request'


export const fetchBulletin = (id) => {
    return request({
        url: '/Bulletin/' + id,
        method: 'get',
    })
}
export const fetchBulletinList = (query) => {
    return request({
        url: '/Bulletins',
        method: 'get',
        params: query
    })
}



