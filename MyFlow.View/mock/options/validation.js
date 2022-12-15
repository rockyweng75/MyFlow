const testCases = 
[
]

const mock = [
    {
        url: '/api/Validation',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock