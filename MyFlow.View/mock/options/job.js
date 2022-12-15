const testCases = 
[
    { Key: '郵件通知申請者', Value: 'A000' },
]

const mock = [
    {
        url: '/api/Job',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


