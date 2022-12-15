const testCases = 
[
    { Key: '{申請人}申請{表單名稱}', Value: 'A000' },
]

const mock = [
    {
        url: '/api/Title',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock