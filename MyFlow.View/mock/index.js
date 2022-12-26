import { createProdMockServer } from 'vite-plugin-mock/es/createProdMockServer';

import testSecurity from '../mock/security';
import testFlowchart from '../mock/flowchart';

import testTodoList from '../mock/workboard/todoList';
import testWaitList from '../mock/workboard/waitList';
import testApproveList from '../mock/workboard/approveList';
import testHistoryList from '../mock/workboard/historyList';
import testProcess from '../mock/workboard/process';

import testActionClass from '../mock/options/actionClass';
import testActionType from '../mock/options/actionType';
import testDataRef from '../mock/options/dataRef';
import testDeadline from '../mock/options/deadline';
import testFlowType from '../mock/options/flowType';
import testItemType from '../mock/options/itemType';
import testJob from '../mock/options/job';
import testSwitch from '../mock/options/switch';
import testTag from '../mock/options/tag';
import testTarget from '../mock/options/target';
import testTitle from '../mock/options/title';
import testValidation from '../mock/options/validation';
import testSwitchClass from '../mock/options/switchClass';
import testFormType from '../mock/options/formType';
import testForm from '../mock/form';
import testWeather from '../mock/weather';


export function setupProdMockServer() {
  createProdMockServer([
    ...testSecurity,
    ...testFlowchart,
    ...testTodoList,
    ...testWaitList,
    ...testApproveList,
    ...testHistoryList,
    ...testProcess,
    ...testActionClass,
    ...testActionType,
    ...testDataRef,
    ...testDeadline,
    ...testFlowType,
    ...testItemType,
    ...testJob,
    ...testSwitch,
    ...testTag,
    ...testTarget,
    ...testTitle,
    ...testValidation,
    ...testSwitchClass,
    ...testFormType,
    ...testForm,
    ...testWeather
  ]);
}