const testCases = 
[
    { Key: '人事系統', Value: 0 },
    { Key: '總務系統', Value: 1 },
]

const mock = [
    {
        url: '/api/FlowType',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


