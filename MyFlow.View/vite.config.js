import { defineConfig } from 'vite';
import vue from "@vitejs/plugin-vue";
import path from 'path'
import { viteMockServe } from 'vite-plugin-mock'



// const myPlugin = () => ({
//   name: 'configure-server',
//   configureServer(server) {
//     server.middlewares.use((req, res, next) => {
//       // custom handle request...
//       console.log(req)

//       // next()
//     })
//   },
// })

export default defineConfig(({command, mode}) => {
  let prodMock = true
  return {
    base: '/MyFlow/',
    mode: command !== 'serve',
    plugins: [
      vue(),
      viteMockServe({
        supportTs: false,
        mockPath: 'mock',
        localEnabled: command === 'serve',
        // prodEnabled: true,
        prodEnabled: command !== 'serve' && prodMock,
        injectCode: `
          import { setupProdMockServer } from '../mock/index';
          setupProdMockServer();
        `,
      })
      // myPlugin()
    ],
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "src"),
      },
    },
    // server: {
    //   proxy: {
    //     '/ws': {
    //       target: 'ws://localhost:5173',
    //       // changeOrigin: true,
    //       ws: true
    //     }
    //   }
    // },
    css: {
      postcss: {
        plugins: [
            {
              postcssPlugin: 'internal:charset-removal',
              AtRule: {
                charset: (atRule) => {
                  if (atRule.name === 'charset') {
                    atRule.remove();
                  }
                }
              }
            }
        ],
      },
    }
  };
});
