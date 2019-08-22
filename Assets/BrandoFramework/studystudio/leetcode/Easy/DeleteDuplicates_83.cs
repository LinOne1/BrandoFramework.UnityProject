﻿#region Head

// Author:            LinYuzhou
// Email:             836045613@qq.com

#endregion

namespace Study.LeetCode
{
    public partial class Solution
    {
        // 83. 删除排序链表中的重复元素
        // 给定一个排序链表，删除所有重复的元素，使得每个元素只出现一次。

        // 示例 1:

        // 输入: 1->1->2
        // 输出: 1->2
        // 示例 2:

        // 输入: 1->1->2->3->3
        // 输出: 1->2->3

        public ListNode DeleteDuplicates(ListNode head) 
        {
            if(head == null)
            {
                return null;
            }
            ListNode result = head;
            while(result.next != null)
            {
                if(result.val == result.next.val)
                {
                    result.next = result.next.next;
                }
                else
                {
                    result = result.next;
                }
            }
            return head;
        }
    }
}


