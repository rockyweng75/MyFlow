import { defineConfig } from 'vite';
import vue from "@vitejs/plugin-vue";
import path from 'path'
import { viteMockServe } from 'vite-plugin-mock'

export default defineConfig(({command, mode}) => {
  let prodMock = true
  return {
    base: '/myflow/',
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
    ],
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "src"),
      },
    },
    build: {
      assetsDir: './'
    },
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
