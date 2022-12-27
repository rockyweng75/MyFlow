const mock = [
  {
      url: '/Weather',
      method: 'get',
      timeout: 2000,
      response: ({ query, body, headers }) => {
        return {
          'Location': 'YunLin',
          'Weather': '晴天',
          'ProbabilityOfPrecipitation': 0.5,
          'Temperature': 31,
          'MaximumTemperature': 32,
          'MinimumTemperature': 12,
          'Icon': 'Sunny',
      };
    },
  }
]
  
export default mock