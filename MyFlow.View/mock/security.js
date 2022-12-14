const tokens = {
    admin: {
      token: "admin-token"
    },
    user: {
      token: "user-token"
    }
  };
  
  const users = {
    "admin-token": {
      user:{
        UserNo: 'A01',
        UserName: "AdminUser",
        Roles: ["admin"],
      }
    },
    "user-token": {
      user:{
        UserNo: 'U01',
        UserName: "User",
        Roles: ["user"],
      }
    }
  };
  
  const mock = [
      {
          url: '/signin',
          method: 'post',
          response: ({ query ,body }) => {
              return tokens.admin.token;
          },
      },
      {
          url: '/user',
          method: 'post',
          timeout: 2000,
          response: ({ query, body, headers }) => {
            return users[body.data];
        },
      },
      {
        url: '/signout',
        method: 'post',
        timeout: 2000,
        response: ({ query, body, headers }) => {
          return 1;
      },
    },
  ]
  
  export default mock