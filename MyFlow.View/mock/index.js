import { createProdMockServer } from 'vite-plugin-mock/es/createProdMockServer';

import testSecurity from '../mock/security';
import testFlowchart from '../mock/flowchart';

import testTodoList from '../mock/workboard/todoList';
import testWaitList from '../mock/workboard/waitList';
import testApproveList from '../mock/workboard/approveList';
import testHistoryList from '../mock/workboard/historyList';
import testProcess from '../mock/workboard/process';


export function setupProdMockServer() {
  createProdMockServer([
    ...testSecurity,
    ...testFlowchart,
    ...testTodoList,
    ...testWaitList,
    ...testApproveList,
    ...testHistoryList,
    ...testProcess
  ]);
}