const testCases = 
[
    { Key: '自訂', Value: 'CustomTarget' },
    { Key: '人事主管', Value: 'PersonnelManagerTarget' },
]

const mock = [
    {
        url: '/api/Target',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


