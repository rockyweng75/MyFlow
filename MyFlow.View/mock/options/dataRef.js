const testCases = 
[
]

const mock = [
    {
        url: '/api/DataRef',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock