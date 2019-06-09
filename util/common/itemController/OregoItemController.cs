using System;
using System.Collections.Generic;

namespace OregoBlink.util.common.itemController
{
    public class OregoItemController<T>
    {
        private readonly List<Func<T, T>> list = new List<Func<T, T>>();

        #region Add

        public void AddTrigger(Handler handler, int position = -1)
        {
            if (position == -1)
            {
                this.list.Add(handler.Update);
            }
            else if (position < this.list.Count)
            {
                this.list.Insert(position, handler.Update);
            }
        }

        public void AddTrigger(Func<T, T> action, int position = -1)
        {
            if (position == -1)
            {
                this.list.Add(action);
            }
            else if (position < this.list.Count)
            {
                this.list.Insert(position, action);
            }
        }

        #endregion

        #region Remove

        public void RemoveTrigger(Func<T, T> action)
        {
            this.list.Remove(action);
        }

        public void RemoveTrigger(Handler handler)
        {
            this.list.Remove(handler.Update);
        }

        #endregion

        #region Update

        public T Update(T inputValue)
        {
            //Init value:
            var currentValue = inputValue;

            try
            {
                //Push value into pipe:
                for (var i = 0; i < this.list.Count; i++)
                {
                    if (i < this.list.Count)
                    {
                        var action = this.list[i];
                        currentValue = action.Invoke(currentValue);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            //Return end value:
            return currentValue;
        }

        #endregion

        #region Handler

        public abstract class Handler
        {
            protected readonly OregoItemController<T> controller;

            protected Handler(OregoItemController<T> controller)
            {
                this.controller = controller;
            }

            public abstract T Update(T value);
        }


        #endregion
    }
}