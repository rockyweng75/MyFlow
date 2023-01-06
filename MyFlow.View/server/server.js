// import express from 'express'
// import { createServer as createViteServer } from 'vite'

// async function createServer() {
//   const app = express()

//   // Create Vite server in middleware mode
//   const vite = await createViteServer({
//     server: { middlewareMode: true },
//     appType: 'custom', // don't include Vite's default HTML handling middlewares
//   })
//   // Use vite's connect instance as middleware
//   app.use(vite.middlewares)

//   app.use('*', async (req, res) => {
//     const url = req.originalUrl
//     try {
//       let template = fs.readFileSync(
//         path.resolve(__dirname, "index.html"),
//         "utf-8"
//       )
//       template = await vite.transformIndexHtml(url, template)
//       res.status(200).set({ "Content-Type": "text/html" }).end(template)
//     } catch (e) {
//       vite.ssrFixStacktrace(e)
//       next(e)
//     }
//   })
// }

// createServer()