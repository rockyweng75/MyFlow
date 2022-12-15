const testCases = 
    [
        { Key: '送出', Value: 0 },
        { Key: '同意', Value: 1 },
        { Key: '不同意', Value: 2 },
        { Key: '取消', Value: 3 },
        { Key: '轉送', Value: 4 },
        { Key: '撤銷', Value: 5 },
    ]


const mock = [
    {
        url: '/api/ActionType',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


