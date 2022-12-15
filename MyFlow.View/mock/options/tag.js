const testCases = 
[
    { Key: '{流程名稱},{申請人}', Value: 'A000' },
]

const mock = [
    {
        url: '/api/Tag',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


