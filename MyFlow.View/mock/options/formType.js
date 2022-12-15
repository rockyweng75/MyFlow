const testCases = 
[
    { Key: '申請單', Value: 0 },
    { Key: '簽核單', Value: 1 },
]
const mock = [
    {
        url: '/api/FormType',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock