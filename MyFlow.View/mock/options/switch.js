const testCases = 
[
    { Key: '全開放', Value: 'AllPase' },
]

const mock = [
    {
        url: '/api/Switch',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


