import { createProdMockServer } from 'vite-plugin-mock/es/createProdMockServer';

import testSecurity from '../mock/security';
import testFlowchart from '../mock/flowchart';

export function setupProdMockServer() {
  createProdMockServer([
    ...testSecurity,
    ...testFlowchart
  ]);
}