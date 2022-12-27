const mock = [
  {
    url: '/Members',
    method: 'get',
    response: ({ query, body, headers }) => {
      return [
        { 
            Name: '陳大成',
            Title: '總經理',
            Status: 'online',
            Department: '',
            TelExt: '100'
        },
        { 
            Name: '王小玫',
            Title: '總經理秘書',
            Status: 'online',
            Department: '',
            TelExt: '100'
        },
        { 
            Name: '吳一天',
            Title: '人事部經理',
            Status: 'busy',
            Department: '',
            TelExt: '200'
        },
        { 
            Name: '李三',
            Title: '人事部部員',
            Status: 'offline',
            Department: '',
            TelExt: '202'
        },
      ];
    },
  }
]
  
export default mock