import { defineConfig } from 'vite';
import vue from "@vitejs/plugin-vue";
import path from 'path'
import { viteMockServe } from 'vite-plugin-mock'
// import commonjsExternals from 'vite-plugin-commonjs-externals';

// import { UserConfigExport, ConfigEnv } from 'vite';

// console.log(path.resolve(__dirname, './src/lang/**'))
export default defineConfig(({command, mode}) => {
  let prodMock = false
  return {
    base: '/myflow/',
    mode: command !== 'serve',
    plugins: [
      vue(),
      viteMockServe({
        supportTs: false,
        mockPath: 'mock',
        localEnabled: command === 'serve',
        prodEnabled: command !== 'serve' && prodMock,
      })
      // viteRequire({
      //   externals: ['path', /^electron(\/.+)?$/]
      // })
    ],
    resolve: {
      alias: {
        "/@": path.resolve(__dirname, "src"),
      },
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
