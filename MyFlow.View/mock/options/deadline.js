const testCases = 
[
]

const mock = [
    {
        url: '/api/Deadline',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock